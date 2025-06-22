# BAIS3230Project

## To-do
1. Add MyReservations page so that when a users logged in they can see only they're own reservations and do CRUD on it. (double check what each role can do)
2. Events need to block off teetime/standingteetime times
3. double-check every validation in entity and make sure they are working on forms
4. UI Design
5. 

MAYBE ADD THIS TO DB

ALTER TABLE Members
ADD CONSTRAINT CK_AccountStatus CHECK (AccountStatus IN ('Good', 'Inactive', 'Suspended'));

## Notes

### Register, login, logout, registermessage information

https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-9.0&tabs=visual-studio

### Disable default account verification
With the default templates, the user is redirected to the Account.RegisterConfirmation where they can select a link to have the account confirmed. The default Account.RegisterConfirmation is used only for testing, automatic account verification should be disabled in a production app.

To require a confirmed account and prevent immediate login at registration, set DisplayConfirmAccountLink = false in /Areas/Identity/Pages/Account/RegisterConfirmation.cshtml.cs

### Notes about validation

**Frontend Validation**:
When you submit the form from your Razor Page, the client-side validation will work if you have the necessary JavaScript libraries enabled (e.g., jQuery, jQuery validation). Razor will automatically apply the data annotations for client-side validation based on the model annotations.

**Back-End Validation**:
When the form is submitted, the ModelState.IsValid check in the OnPost method will ensure that the data passed to the backend meets the criteria you've defined with the annotations. If ModelState is invalid, you can return the page with the validation errors displayed to the user.

### Things to research

Only certain users will be able to access certain pages on the application. I need to figure out how to properly assign roles and allow only certain users to access certain parts of the site.

### Important note about Razor CRUD scaffolding

This scaffolding does not see relationships if you did not use EF Core power tools to create your entities, etc. 
To be able to properly use CRUD and scaffold it properly you needed to use this tool before hand so that it creates relationships between your entities.

## Useful links

### Page for identity for asp.net
https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-9.0&tabs=visual-studio

### Policy authentication for identity in razor pages

https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-9.0#apply-policies-to-razor-pages


###	use this link to setup scaffolded pages
"https://learn.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-9.0&tabs=visual-studio#scaffold-identity-into-a-razor-project-with-authorization"

### Blazor quickgrid(for capstone)
https://learn.microsoft.com/en-us/aspnet/core/blazor/components/quickgrid?view=aspnetcore-9.0&tabs=visual-studio

