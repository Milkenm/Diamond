namespace DiamondTesting
{
	[TestClass]
	public class TestTest
	{
		private IUser? user;
		private IMessage? message;

		[TestInitialize]
		public void InitializeTest()
		{
			Mock<IMessage> mockMessage = new Mock<IMessage>();
			Mock<IUser> mockUser = new Mock<IUser>();

			_ = mockUser.SetupGet(u => u.Username).Returns("test");
			_ = mockMessage.SetupGet(msg => msg.Author).Returns(mockUser.Object);

			this.user = mockUser.Object;
			this.message = mockMessage.Object;
		}

		[TestMethod]
		public void Test()
		{
			Assert.AreEqual("test", this.message!.Author.Username);
		}
	}
}