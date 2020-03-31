using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace One1Lion.BlazorComponents.DragAndDrop {
  public class DnDState<TItem> {
    public DnDState() {
      Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }
    public IDnDContainer<TItem> BaseContainer { get; set; }
    public DnDPayload<TItem> Payload { get; set; }
    public DnDPayload<TItem> Target { get; set; }
    public DnDPayload<TItem> NewItemPayload { get; set; }
    public Func<TItem> NewGroupMethod { get; set; }
    public Func<TItem> NewItemMethod { get; set; }
    public TItem NewItem { get; set; }

    public string ChildrenPropertyName { get; set; }

    public event Action OnNotifyStateChanged;

    public void SetPayload(IDnDContainer<TItem> parent, TItem item, IDnDElement<TItem> wrappingElement) {
      SetPayload(parent, parent?.Children?.IndexOf(item) ?? -1, wrappingElement);
    }

    public void SetPayload(IDnDContainer<TItem> parent, int indexInParent, IDnDElement<TItem> wrappingElement) {
      Payload = new DnDPayload<TItem>() {
        Parent = parent,
        IndexInParent = indexInParent,
        WrappingElement = wrappingElement
      };
      NotifyStateChanged();
    }

    public void ClearPayload() {
      Payload = default;
      Target = default;
      NotifyStateChanged();
    }


    public void SetTarget(IDnDContainer<TItem> parent, TItem item, IDnDElement<TItem> wrappingElement, bool isDropFirst = true) {
      SetTarget(parent, parent?.Children?.IndexOf(item) ?? -1, wrappingElement, isDropFirst);
    }

    public void SetTarget(IDnDContainer<TItem> parent, int indexInParent, IDnDElement<TItem> wrappingElement, bool isDropFirst = true) {
      Target = new DnDPayload<TItem>() {
        Parent = parent,
        IndexInParent = indexInParent,
        WrappingElement = wrappingElement,
        GroupAsFirst = isDropFirst
      };
      NotifyStateChanged();
    }

    public void ClearTarget() {
      Target = default;
      NotifyStateChanged();
    }

    public void ShowNewItem(IDnDContainer<TItem> parent, TItem item, IDnDElement<TItem> wrappingElement) {
      ShowNewItem(parent, parent?.Children?.IndexOf(item) ?? -1, wrappingElement);
    }

    public void ShowNewItem(IDnDContainer<TItem> parent, int indexInParent, IDnDElement<TItem> wrappingElement) {
      NewItemPayload = new DnDPayload<TItem>() {
        Parent = parent,
        IndexInParent = indexInParent,
        WrappingElement = wrappingElement
      };
      NewItem = NewItemMethod is { } ? NewItemMethod.Invoke() : default;
      NotifyStateChanged();
    }

    public void ClearNewItem() {
      NewItemPayload = default;
      NewItem = default;
      NotifyStateChanged();
    }

    public void HandleDropPayload(bool dropToGroup = false) {
      if (Payload is null || Target?.Parent is null) {
        // Nothing to drop
        // TODO: Report message
        return;
      }

      if (Payload.IsContainer && Payload.Parent is { } && Target.Parent.Address.StartsWith(Payload.WrappingElement.Address)) {
        // Cannot drop a parent group into a child group
        return;
      }

      var sameParent = Payload.Parent == Target.Parent;
      if (sameParent && Target.IndexInParent == Payload.IndexInParent) {
        // Dropping an element into its original index, so nothing to do
        return;
      }

      // Get the Payload item in the original parent
      var payloadItem = Payload.Item;
      // TODO: Verify group is allowed if the dropToGroup argument is true
      if (dropToGroup && NewGroupMethod is { }) {
        // Target Item is only relevant when grouping
        var targetItem = Target.Item;
        var newGroup = NewGroupMethod.Invoke();
        var childProp = string.IsNullOrWhiteSpace(ChildrenPropertyName) ? null : newGroup.GetType().GetProperty(ChildrenPropertyName);
        if (childProp is null) {
          // TODO: Handle null child property of new group
          throw new MissingMemberException($"The specified NewGroupMethod produced an object that does not have a Children property with the specified ChildrenPropertyName ({ChildrenPropertyName})");
        }
        var children = new List<TItem>();
        children.Add(Target.GroupAsFirst ? payloadItem : targetItem);
        children.Add(!Target.GroupAsFirst ? payloadItem : targetItem);
        childProp.SetValue(newGroup, children);
        Target.Parent.Children.RemoveAt(Target.IndexInParent);
        Target.Parent.Children.Insert(Target.IndexInParent, newGroup);
      } else {
        Target.Parent.Children.Insert(Target.IndexInParent, payloadItem);
      }
      // Remove the item from the original parent if it has a parent (new items might not have parents)
      if (Payload.Parent is { }) {
        // Offset the index to remove the payload from its parent by +1 if the item is being dropped in the same parent at an index higher than its original index
        var removeIndex = sameParent && !dropToGroup ? Payload.IndexInParent + (Payload.IndexInParent > Target.IndexInParent ? 1 : 0) : Payload.IndexInParent;
        Payload.Parent.Children.RemoveAt(removeIndex);
        // Check to see if the Payload's previous parent is not the BaseContainer and is either empty or has only one child
        var parentsContainer = Payload.Parent.Parent;
        CheckForUngroupOrDelete(Payload.Parent, parentsContainer == Target.Parent && Target.IndexInParent <= Payload.Parent.IndexInParent && !dropToGroup ? 1 : 0);
      }

      //notifyChange:
      NotifyStateChanged();
    }

    public void HandleDeleteItem(IDnDContainer<TItem> parent, int indexInParent) {
      if (parent is null || indexInParent < 0 || indexInParent >= (parent.Children?.Count ?? 0)) {
        // The item does not have a parent or the index is out of bounds
        // TODO: Report a message
        return;
      }

      parent.Children.RemoveAt(indexInParent);
      // Check for ungroup/remove
      CheckForUngroupOrDelete(parent);

      NotifyStateChanged();
    }

    public void HandleDeleteItem(IDnDElement<TItem> item) {
      if (item.Parent?.Children is null) {
        // The item does not have a parent or the supplied parent does not have children
        // TODO: Report a message
        return;
      }
      if (string.IsNullOrWhiteSpace(item.Address) || !int.TryParse(item.Address.Split('.').Last(), out int indexInParent)) {
        // The address is either missing or it does not end in an integer (which means it could be the base container that cannot be deleted)
        // TODO: Report a message
        return;
      }
      HandleDeleteItem(item.Parent, indexInParent);
    }

    private void CheckForUngroupOrDelete(IDnDContainer<TItem> container, int indexOffset = 0) {
      if (container != BaseContainer) {
        var parentsContainer = container.Parent;
        var index = container.IndexInParent + indexOffset;
        if (container.IndexInParent < 0) {
          // TODO: Handle invalid index in Parent's Parent container
          return;
        }
        if (container.Children.Count == 1) {
          // There is only one child, so it should be "ungrouped"
          // Remove the element at the index in the Payload's Parent's Parent (which is the Payload's previous Parent)
          parentsContainer.Children.RemoveAt(index);
          // Add the child to the Payload's Parent's Parent at the Payload's current/new Address
          parentsContainer.Children.Insert(index, container.Children[0]);
        } else if (container.Children.Count == 0) {
          // Remove the element from its parent container
          // Use the address to get the index in the Parent 
          // -- NOTE: we might have an issue when the Target is in the same Parent as the Payload's Parent's Container
          parentsContainer.Children.RemoveAt(index);
        }
      }
    }

    public void NotifyStateChanged() {
      OnNotifyStateChanged?.Invoke();
    }
  }
}
