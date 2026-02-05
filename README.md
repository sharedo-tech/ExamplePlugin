# ShareDo/Operate Example Plugin

This plugin contains samples of common patterns for extending ShareDo/Operate.

# Contents

- [ODS External Search Provider](./Samples/OdsExternalSearchProvider/)


# Running The Plugin

> [!IMPORTANT]
> You must have a working instance of Sharedo In A Box (SIAB) to run this plugin

To use this plugin locally:
1. Create a 'Plugins' folder in the root of your SIAB folder:

   <img width="283" height="178" alt="image" src="https://github.com/user-attachments/assets/897f8dbe-0ccd-4a8a-88cc-39baabc6f6c6" />

2. Clone this repository in to this folder. The resulting struture should be
   ```
   c:\
     ─ {SIAB folder}
       ─ Plugins
         ─ ExamplePlugin
           ─ ExamplePlugin.sln
             (etc.)
   ```

3. Ensure ShareDo In A Box is running
4. Open `ExamplePlugin.sln` and build it. The solution contains a post-build hook that will install the plugin and restart the application pool
5. Open ShareDo In A Box - the plugin should now be installed
