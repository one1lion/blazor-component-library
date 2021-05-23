# blazor-component-library
Collection of Component Libraries used in various projects

## Caution - WIP
This library is still a work in process.  I will continue updating it as I find time.  Most of the components have at least one implementation approach or styling or some other issue that can be improved or finished.  I also plan to create NuGet packages as releases when the project becomes more stable.  Moreover, I plan to change the css and JavaScript Interop usage into Isolated CSS and JavaScript to remove the step for manually adding the needed CSS and JavaScript references to index/\_Host.  I also want to remove the dependence on Bootstrap styles for those components that use it.

# Quick Summary
The One1Lion.Components project contains the component library.  Each Component type is contained within its own project.  The Shared project is for the Interfaces, Utilities, Shared Components, and other code shared throughout the Components project.

# Getting Started

To use the Component Library, for now, clone the repo and add the projects to your code base.  As mentioned before, I will generate NuGet files s\*\*n.  After adding references to the included projects from the project you'd like to use the components in, either to index.html (WASM) or \_Host.cshtml (Server), add:

`<script src="_content/One1Lion.Shared/js/o1lJsInterop.js"></script>` 

_Note: This can also be added in the `head` tag instead of at the end of the `body` tag with the deferred attribute to speed up loading times_

_Another Note: I plan to use exported js modules in the future to eliminate the need to add this manually_

For any components that you'd like to use, you can add their CSS links (available in Minified and ...not...minified) individually, e.g.:

`<link rel="stylesheet" href="_content/One1Lion.Dialog/css/dialog.min.css" />`,

or add the full CSS for all components:

`<link rel="stylesheet" href="_content/One1Lion.BlazorLib/css/o1l-components.min.css" />`,

_Note: I plan to use Isolated CSS to eliminate the need to add this manually_

# Samples

The One1Lion.Samples project is the startup Blazor WASM (not hosted/client only) project.  The Pages folder has what you would expect (the main routable Blazor pages), as well as the components that contain the sample usages of the components, each type separated into a subfolder.  Up a level from the Pages folder are the Components (for components only used by the Blazor sample project) and the Sass (the SCSS files) folders.  

For the SCSS files: the only one configured to compile is the Site.scss file, while the FromNewProject.scss is a copy/pasta of the original site.css.  The CustomStyles.scss contains the css rules that override or are in addition to the FromNewProject.scss.  The Site.scss file may need to be told to manually recompile since it does not always recompile when making a change to one of the imported files.  

The One1Lion.Samples.SharedLib project is used for code (Utils, POCOs, etc) that is shared through the samples (in case I add a Blazor WASM version of samples).  I plan on adding the WASM samples as a separate project as well, with the Server-Only and WASM flavors sharing a component library for the UI.  That would be adding a .Pages project for the shared routable pages and components that are specific to the samples pages, and the client-side project that can be started separated from the server project.

In any case, that is the quick run through of what the structure is for this solution.  More to come.
