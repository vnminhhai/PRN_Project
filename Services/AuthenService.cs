using PRN_Project_Summer_2024.Models;

namespace PRN_Project_Summer_2024.Services;

public class AuthenService
{
    private PrnProjectSummer2024Context context = PrnProjectSummer2024Context.Context;

    public User? login(string username, string password)
    {
        try
        {
            return context.Users.Single(u => u.Name.Equals(username) && u.Password.Equals(password));
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public User? signup(User u)
    {
        return context.Users.Add(u).Entity;
    }
}