# blazor-component-library
Collection of Component Libraries used in various projects

# Quick Summary
The One1Lion.Components project contains the component library.  Each Component is contained within its own folder.  The SharedLib folder is for the Interfaces, Utilities, Shared Components, and other code shared throughout the Components project.  

The One1Lion.Samples project is the startup Blazor server project.  The Pages folder has what you would expect (the main routable Blazor pages), as well as the components that contain the sample usages of the components, each type separated into a subfolder.  Up a level from the Pages folder are the Components (used for components only used by the Blazor project) and the Sass (the SCSS files) folders.  

For the SCSS files: the only one configured to compile is the Site.scss file, while the FromNewProject.scss is a copy/pasta of the original site.css.  The CustomStyles.scss contains the css rules that override or are in addition to the FromNewProject.scss.  The Site.scss file may need to be told to manually recompile since it does not always recompile when making a change to one of the imported files.  

The One1Lion.SharedLib project is used for code (Utils, POCOs, etc) that is shared through the entire solution.  Although, now that I think of it, the One1Lion.Components project does not use the code from it, but it will be there in case I decide to add a Blazor WASM version of the samples.  That would be adding a .Pages project for the shared routable pages and components that are specific to the samples pages, and the client-side project that can be started separated from the server project.

In any case, that is the quick run through of what the structure is for this solution.  More to come.
