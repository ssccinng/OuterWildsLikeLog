public static class SystemStateManager
{
    // 撕烤状态管理，模式切换等
    public static SystemMode SystemMode = SystemMode.DragMode;


}


public enum SystemMode {
    LookMode,
    DragMode,
    LineMode,
}