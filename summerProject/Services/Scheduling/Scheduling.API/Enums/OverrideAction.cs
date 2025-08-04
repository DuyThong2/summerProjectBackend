namespace Scheduling.API.Enums
{
    public enum OverrideAction
    {
        Skip,       // bỏ món này
        Replace,    // thay bằng món khác
        Add         // thêm (trường hợp add bạn có thể dùng AdHocMeal, nhưng cho đủ thì vẫn để)
    }
}
