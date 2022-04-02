namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误
        
        // 110000以下的错误请看ErrorCore.cs
        
        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        // 200001以上不抛异常


        public const int ERR_NetWorkError = 200002;   
        
        public const int ERR_LoginInfoError = 200003;  //登录信息错误
        
        public const int ERR_AccountNameFormatError = 200004;  //登录账号格式错误
        
        
        public const int ERR_PasswordFormatError = 200005;  //登录密码格式错误
        
        public const int ERR_AccountInBlackListError = 200006;  // 账号处于黑名单中
        
        
        public const int ERR_LoginPasswordError = 200007;  //登录密码错误
        public const int ERR_RequestRepeatError = 200008;  //登录请求重复
        
        public const int ERR_TokenError = 200009;  //登录令牌错误
        public const int ERR_RoleNameIsNull = 200010;  //角色名字为空
        public const int ERR_RoleNameSame = 200011;  //角色名字相同
        public const int ERR_RoleNotExist = 200012; //角色不存在
        public const int ERR_RequestSceneTypeError = 200013; // 请求的sceneType 错误
        
        public const int ERR_ConenctGateKeyError = 200014; // 连接Gate令牌 错误
        
        public const int ERR_OtherAccountLogin = 200015; // 账号再其他地方登录
        
        public const int ERR_SessionPlayerError = 200016; // 账号再其他地方登录
        
        public const int ERR_NonePlayerError = 200017; // 账号再其他地方登录
        
        public const int ERR_SessionStateError = 200018; // 账号再其他地方登录
        
        public const int ERR_EnterGameError = 200019; // 进入逻辑服失败
    }
}