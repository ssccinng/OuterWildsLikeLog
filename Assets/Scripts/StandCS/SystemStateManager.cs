public static class SystemStateManager
{
    // 撕烤状态管理，模式切换等
    public static SystemMode SystemMode = SystemMode.DragMode;


    // 切换模式后要考虑做更多的事

}


public enum SystemMode {
    LookMode,
    DragMode,
    LineMode,
    NodeEditMode,
}