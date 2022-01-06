# RadarLite
Fast, interactive radar and weather application; it is far from complete.
This application is intened to be an ongoing, long term solution to having access to unified weather information quickly. 
Use your best discretion and feel free to propose any sort of change in the route of a pull request.
For more information on contriubting, view the CONTRIBUTING.md file

# Current Status
The application is currently functionable but very early in development. Future features will be noted. 


# Setting it up
 It is reccommended to use Visual Studio and one more 'lightweight' IDE of your choice (VS code, JetBrains, etc...).
 
 You'll need the .NET 6.0 SDK.
 It is also reccommended that you have a ~somewhat~ understanding of minimal API concepts.
 
 # Server side
 After successfully loading the repo in your IDE, set the startup projects to RadarLite.NationalWeatherService, RadarLite.Identity, RadarLite.Web.
 
 ![image](https://user-images.githubusercontent.com/31714144/147886985-75c06006-481e-475a-96af-2694e95b15a4.png)
 
 Ports for these projects should be configured or checked in the appsettings.json. Default ports have been specifed.
 
 Install dependencies.
 
 Run migrations against a Microsoft sql server database.
 
 # Client side
 With your lightweight IDE, navigate to the project folder > RadarLite.Web > client. The client folder is what should be opened in the IDE. 
 
 ![image](https://user-images.githubusercontent.com/31714144/147887300-610a8547-df06-4884-80f1-94be0603a7a6.png)

 run 'yarn add' to install dependencies from the yarn.lock file. 
 
 The package.json has been configured to run at the same port as RadarLite.Web.
 
 If you are choosing to setup IIS for development, run the serve script (vue-cli-service serve --port 7504) followed by running your server side projects.
 
 If you are not using IIS for dev purposes, configure either the RadarLite.Web project to not run at start up or simply change either the client port or RadarLite.Web port.
 
 # Miscellaneous notes from the developer
 
 This project is not close to finished (unfortunately). There are big plans to keep pushing RadarLite for a long time! I hope whomever is reading this gets some value out of the
 project; whether its the code, formating, or just overall being able to see a weather application like this. This project has been a personal interest for a very long time and now finally is coming into reality. So if you've made it this far, thank you! My mission is to give back to the community so much of what I have learned and hopefully one day have an application that is completely functionable. And hopefully meet some new people and continue to learn along the way - J
 
 Develop!
