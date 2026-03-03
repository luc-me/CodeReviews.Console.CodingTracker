# Coding Tracker App
This app allows users to track their coding sessions by logging the start time and the end time of the session.

## Given requeriments
- This application has the same requirements as the previous project, except that now you'll be logging your daily coding time.
- To show the data on the console, you should use the Spectre.Console library.
- You're required to have separate classes in different files (i.e. UserInput.cs, Validation.cs, CodingController.cs)
- You should tell the user the specific format you want the date and time to be logged and not allow any other format.
- You'll need to create a configuration file called appsettings.json, which will contain your database path and connection strings (and any other configs you might need).
- You'll need to create a CodingSession class in a separate file. It will contain the properties of your coding session: Id, StartTime, EndTime, Duration. When reading from the database, you can't use an anonymous object, you have to read your table into a List of CodingSession.
- The user shouldn't input the duration of the session. It should be calculated based on the Start and End times
- The user should be able to input the start and end times manually.
- You need to use Dapper ORM for the data access instead of ADO.NET. (This requirement was included in Feb/2024)
- Follow the DRY Principle, and avoid code repetition.
- Don't forget the ReadMe explaining your thought process.

## Project Struct
- DatabaseManager : This class can access to the database using Dapper which is a basic ORM.
- UIhandler : This class shows the UI and manage the user's input. Use Spectre.Console.
- Controller : This class connect the database with the UI and add the logic the app.
- MenuOptions : This is a enum used by UIhandler and controller to comunicate which option was selected.
- Coding : This class represent a coding session with the same properties of the table from the database.
- Program : This is the principal file, with the Main method. 

## Improvements
- Add date to the properties of coding session.
- Add style to UI texts.
- Add a real time tracking function

## Reflections and challenges
This project is similar to the habit tracker so it was easy the part of the crud operations besides, dapper simplifies these operations. I think the most difficult part was setting the project to run in vscode instead of visual studio. I feel comfortable with the oop, but I have the thinking that I'm using the classes incorrectly. I will apreciate the feedback.