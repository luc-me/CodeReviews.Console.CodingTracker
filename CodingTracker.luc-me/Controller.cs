
namespace CodingTracker.luc_me;

public class Controller
{
    DatabaseManager database = new();
    UIhandler ui = new();
    public void MenuHandler()
    {
        database.CreateTable();
        ui.Flyer();

        bool endApp = false;
        while (!endApp)
        {
            MenuOptions op = ui.MainMenu();

            switch (op)
            {
                case MenuOptions.Insert_session:
                    AddSession();
                    break;
                case MenuOptions.See_tracked_sessions:
                    ViewAll();
                    break;
                case MenuOptions.Delete_session:
                    DeleteSession();
                    break;
                case MenuOptions.Update_session:
                    UpdateSession();
                    break;
                case MenuOptions.Close_app:
                    endApp = true;
                    break;
                default:
                    break;

            }
        }
    }
    private void AddSession()
    {
        string duration;
        string startTime;
        string endTime;
        do
        {
            startTime = ui.GetTimeInput("\nPlease insert the time you start coding: (Format: H:mm). Type 0 to return main menu.");
            endTime = ui.GetTimeInput("\nPlease insert the time you end coding: (Format: H:mm). Type 0 to return main menu.");
         
        }
        while (!ui.TryGetDuration(startTime, endTime, out duration));
        

        Coding session = new();
        session.StartTime = startTime;
        session.EndTime = endTime;
        session.Duration = duration;

        database.Insert(session);

        ui.CleanConsole();
    }
    private void ViewAll()
    {
        var sessions = database.Get();
        ui.ShowTable(sessions);

        ui.CleanConsole();
    }

    private void DeleteSession()
    {
        var sessions = database.Get();
        var selectedSessions = ui.SelectList(sessions);
        database.Delete(selectedSessions);
    }

    private void UpdateSession()
    {
        var sessions = database.Get();
        var selectedSessions = ui.SelectList(sessions);
        string duration="";
        foreach (Coding session in selectedSessions)
        {
            do
            {
                ui.PrintSession(session);
                session.StartTime = ui.GetTimeInput("\nPlease insert the time you start coding: (Format: H:mm). Type 0 to return main menu.");
                session.EndTime = ui.GetTimeInput("\nPlease insert the time you end coding: (Format: H:mm). Type 0 to return main menu.");
            
            }
            while (!ui.TryGetDuration(session.StartTime, session.EndTime, out duration));

            session.Duration = duration;
        }

        database.Update(selectedSessions);
    }

    
}
