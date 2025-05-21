namespace ConsoleProject.NET.Exceptions;

public class NoteNotFoundException(int id) : Exception ($"Note with that id {id} not found");
public class UserNotFoundException(int id) : Exception ($"User with that id {id} not found");

public class NameIsRequired : Exception
{
    public NameIsRequired() : base($"Name is required") { }
}
public class TitleIsRequired : Exception
{
    public TitleIsRequired() : base($"Title is required") { }
}