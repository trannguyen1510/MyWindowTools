namespace RightClickShells
{
    public enum RightClickShellType
    {
        Null=-1,
        DirectoryShell=0,
        ExecutableShell=1
    }
    public enum RightClickShellActionType
    {
        Delete=-1,
        Add=1,
        ChangeCommand = 2,
        ChangeName = 3
    }
}
