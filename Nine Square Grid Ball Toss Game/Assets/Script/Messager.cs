using System;

public static class Messenger
{
    // 定义一个委托，用于表示事件处理方法的签名
    public delegate void OnScoreUpdateHandler(int score);

    // 定义一个事件，使用上面的委托作为类型
    public static event OnScoreUpdateHandler OnScoreUpdate;

    // 发送分数更新事件的方法
    public static void SendScoreUpdate(int score)
    {
        // 检查是否有订阅者，然后触发事件
        OnScoreUpdate?.Invoke(score);
    }
}