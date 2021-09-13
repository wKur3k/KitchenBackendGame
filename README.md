# KitchenBackendGame
Kitchen Backend is a WebApi written in C# with frontend(WIP) written in HTML, JS and CSS

There are 3 main panels:
  -Moderator panel in which you can manage users
  -Admin panel in which you can manage accounts
  -User panel in which you can use this app normally(WIP)

/swagger will get you to the swagger page

There are 3 accounts seeded: "admin", "moderator" and "user" and their password is "password".

To authorize EP you have to set value of "Authorization" header to "bearer [TOKEN]" where [TOKEN] is response from login EP
