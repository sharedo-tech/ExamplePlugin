# ODS External Search Provider

This is a working sample of a custom ODS External Search Provider.

This is commonly used to search third-party systems for people/organisations; for example a PMS or CRM.

# Files

| File  | Purpose  |
|---|---|
| `ExampleOdsSearchProvider.cs`  | This is the provider implementation. It is responsible for calling the third-party system and collating the results  |
| `ExampleOdsSearchProviderConfig.cs`  | This is the model representing any custom configuration needed for the provider  |
| `./_Content/OdsExternalSearchProvider/*`  | These files contain an example of a custom configuration UI for the provider |

# How To Use The Provider

The provider will appear in 'Global Features' under the 'ODS External Search' feature.

1. Open the feature, click the `+` button, and select the 'Example Provider'.

   It will be added to the list of providers:

   <img width="640" height="148" alt="image" src="https://github.com/user-attachments/assets/81ece33b-de52-4b19-81ae-fb61ad972a9d" />

2. Clicking the cogs icon will open the custom configuration panel.
   
   Enter some text into this box, Save & Close twice, and recycle the Modeller configuration.

3. Create a new ODS Organisation (`/admin/organisationList` -> "Add New")
   
   The provider appears in the 'Create from..' menu. Select it:
   
   <img width="397" height="162" alt="image" src="https://github.com/user-attachments/assets/5e1969a6-32e0-431b-ba24-1c4457b21c6c" />

4. Enter any search string and press Enter.

   The provider will return a single result (`Organisation 1`), prefixed with the text we entered in our custom configuration (step 2).

   <img width="540" height="255" alt="image" src="https://github.com/user-attachments/assets/5f239151-c48a-4dab-afb9-d2542b1d2bc1" />
