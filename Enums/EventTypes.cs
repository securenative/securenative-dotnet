using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SecureNative.SDK.Enums
{
    public enum EventTypes
    {
        [Description("sn.user.login")]
        LOG_IN,
        [Description("sn.user.login.challenge")]
        LOG_IN_CHALLENGE,
        [Description("sn.user.login.failure")]
        LOG_IN_FAILURE,
        [Description("sn.user.logout")]
        LOG_OUT,
        [Description("sn.user.signup")]
        SIGN_UP,
        [Description("sn.user.auth.challange")]
        AUTH_CHALLENGE,
        [Description("sn.user.auth.challange.success")]
        AUTH_CHALLENGE_SUCCESS,
        [Description("sn.user.auth.challange.failure")]
        AUTH_CHALLENGE_FAILURE,
        [Description("sn.user.2fa.disable")]
        TWO_FACTOR_DISABLE,
        [Description("sn.user.email.update")]
        EMAIL_UPDATE,
        [Description("sn.user.password.reset")]
        PASSWORD_RESET,
        [Description("sn.user.password.reset.success")]
        PASSWORD_RESET_SUCCESS,
        [Description("sn.user.password.update")]
        PASSWORD_UPDATE,
        [Description("sn.user.password.reset.failure")]
        PASSWORD_RESET_FAILURE,
        [Description("sn.user.invite")]
        USER_INVITE,
        [Description("sn.user.role.update")]
        ROLE_UPDATE,
        [Description("sn.user.profile.update")]
        PROFILE_UPDATE,
        [Description("sn.user.page.view")]
        PAGE_VIEW,
        [Description("sn.verify")]
        VERIFY
    }
}
