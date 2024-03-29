﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace One1Lion.ChartJs.Common {
  internal class IgnoreDatasetCountContractResolver : DefaultContractResolver {
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization) {
      IList<JsonProperty> baseProps = base.CreateProperties(type, memberSerialization);

      if (typeof(IDataset).IsAssignableFrom(type)) {
        string countName = nameof(ICollection.Count);
        if (NamingStrategy != null) {
          countName = NamingStrategy.GetPropertyName(countName, false);
        }

        foreach (var prop in baseProps) {
          if (prop.PropertyName == countName &&
              prop.DeclaringType.IsGenericType &&
              prop.DeclaringType.GetGenericTypeDefinition() == typeof(Collection<>)) {
            prop.Ignored = true;
            break;
          }
        }
      }

      return baseProps;
    }
  }
}
