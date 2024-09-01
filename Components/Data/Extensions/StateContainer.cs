using Microsoft.AspNetCore.Authorization;

namespace AttendanceManagement.Extensions
{
	using Microsoft.AspNetCore.Authorization;

    public class AuthorizeByGroupNameAttribute : AuthorizeAttribute
    {
		public AuthorizeByGroupNameAttribute(string groupName)
        {
            Policy = groupName;
            ;
        }
    }
    public class StateContainer
	{
		//private MyItems MyItems;
		public StateContainer()
        {
          //  MyItems = myItems;


		}
        public string PolicyName { get; set; }

        public event Action OnChange;

        public void SetPolicyName(string policyName)
        {
            PolicyName = policyName;
            NotifyStateChanged();
        }

        

        private void NotifyStateChanged() => OnChange?.Invoke();
    }




}
