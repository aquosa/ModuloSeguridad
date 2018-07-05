namespace WebAppControlDualLogin.Model
{
    public class mSession
    {
        public string Id { get; private set; }
        public string User { get; private set; }

        public mSession(string id, string user)
        {
            this.Id = id;
            this.User = user;
        }
    }
}