
public class Education
{
    private WindowReference _windowReference;

    public Education(WindowReference windowReference)
    {
        _windowReference = windowReference;
    }

    public void Display()
    {
        _windowReference.Display();
    }
    
}
