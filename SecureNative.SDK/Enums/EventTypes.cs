using System.ComponentModel;

namespace SecureNative.SDK.Enums
{
    public static class EventTypes
    {
        public static string LOG_IN = "sn.user.login";
        public static string LOG_IN_CHALLENGE = "sn.user.login.challenge";
        public static string LOG_IN_FAILURE = "sn.user.login.failure";
        public static string LOG_OUT = "sn.user.logout";
        public static string SIGN_UP = "sn.user.signup";
        public static string AUTH_CHALLENGE = "sn.user.auth.challenge";
        public static string AUTH_CHALLENGE_SUCCESS = "sn.user.auth.challenge.success";
        public static string AUTH_CHALLENGE_FAILURE = "sn.user.auth.challenge.failure";
        public static string TWO_FACTOR_DISABLE = "sn.user.2fa.disable";
        public static string EMAIL_UPDATE = "sn.user.email.update";
        public static string PASSWORD_RESET = "sn.user.password.reset";
        public static string PASSWORD_RESET_SUCCESS = "sn.user.password.reset.success";
        public static string PASSWORD_UPDATE = "sn.user.password.update";
        public static string PASSWORD_RESET_FAILURE = "sn.user.password.reset.failure";
        public static string USER_INVITE = "sn.user.invite";
        public static string ROLE_UPDATE = "sn.user.role.update";
        public static string PROFILE_UPDATE = "sn.user.profile.update";
        public static string PAGE_VIEW = "sn.user.page.view";
        public static string VERIFY = "sn.verify";
    }
}
