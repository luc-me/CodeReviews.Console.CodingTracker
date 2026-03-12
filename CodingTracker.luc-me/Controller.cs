
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
            ui.CleanConsole();
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
            if (startTime == "0")
                return;
            endTime = ui.GetTimeInput("\nPlease insert the time you end coding: (Format: H:mm). Type 0 to return main menu.");
            if (endTime == "0")
                return;
        }
        while (!ui.TryGetDuration(startTime, endTime, out duration));
        

        Coding session = new();
        session.StartTime = startTime;
        session.EndTime = endTime;
        session.Duration = duration;

        database.Insert(session);
    }
    private void ViewAll()
    {
        var sessions = database.Get();
        ui.ShowTable(sessions);

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
                if (session.StartTime == "0")
                    return;
                session.EndTime = ui.GetTimeInput("\nPlease insert the time you end coding: (Format: H:mm). Type 0 to return main menu.");
                if (session.EndTime == "0")
                    return;
            }
            while (!ui.TryGetDuration(session.StartTime, session.EndTime, out duration));

            session.Duration = duration;
        }

        database.Update(selectedSessions);
    }

    
}
