namespace UserModel
{
	public class User
	{
		private int id;
		private string password;
		private string email;
		private string username;
		private bool isAdmin;

		public User(int id, string password, string email,string username, bool isAdmin)
		{
			this.id = id;
			this.password = password;
			this.email = email;
			this.username = username;
			this.isAdmin = isAdmin;
		}

		public User()
		{
			this.id = 0;
			this.password = "--";
			this.email = "--";
			this.username = "--";
			this.isAdmin = false;
		}

		public int GetId()
		{
			return this.id;
		}

		public void SetId(int id)
		{
			this.id = id;
		}

		public string GetPassword()
		{
			return this.password;
		}

		public void SetPassword(string newPassword)
		{
			this.password = newPassword;
		}

		public string GetEmail()
		{
			return this.email;
		}

		public void SetEmail(string newEmail)
		{
			this.email = newEmail;
		}

		public string GetUsername()
		{
			return this.username;
		}

		public void SetUsername(string newUsername)
		{
			this.username = newUsername;
		}

		public bool GetAdminPrivileges()
		{
			return this.isAdmin;
		}

		public void SetAdminPrivileges(bool newAdmin)
		{
			this.isAdmin = newAdmin;
		}

		public string UserToJson()
		{
			string uPassword = "\"password\": \"" + this.password + "\",";
			string uUsername = "\"username\": \"" + this.username + "\",";
			string uAdmin = "\"isAdmin\": " + this.isAdmin.ToString().ToLower();
			return "{" + uPassword + uUsername + uAdmin + "}";
		}
	}
}

