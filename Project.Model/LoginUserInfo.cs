using System;

namespace Project.Model
{
    [Serializable]
    public class LoginUserInfo
    {
        private int userId;
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private string userName;
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string roleId;
        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }
    }
}
