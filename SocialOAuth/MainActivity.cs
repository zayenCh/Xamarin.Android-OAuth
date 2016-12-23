using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Xamarin.Auth;

namespace SocialOAuth
{
	[Activity(Label = "SocialOAuth", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.twitterButton);
			Button buttonFB = FindViewById<Button>(Resource.Id.facbookButton);
			Button buttonLinkedin = FindViewById<Button>(Resource.Id.LinkedInButton);


			button.Click += delegate { TwitterLogin(); };
			buttonFB.Click += delegate { FacebookLogin(); };
			buttonLinkedin.Click += delegate { LinkedInLogin();};
		}

		void LinkedInLogin()
		{
			var authLinkedIn = new OAuth2Authenticator(
				clientId: "778uah1lhsen62",
				clientSecret: "6jTfDSgTjWiw5UyA",
				scope: "",
				authorizeUrl: new Uri("https://www.linkedin.com/uas/oauth2/authorization"),
				redirectUrl: new Uri("https://www.linkedin.com"),
				accessTokenUrl: new Uri("https://www.linkedin.com/uas/oauth2/accessToken"));
			authLinkedIn.Completed += AuthLinkedIn_Completed;;
			StartActivity(authLinkedIn.GetUI(this));
		}

		void FacebookLogin()
		{
			var authFB = new OAuth2Authenticator(
				clientId: "192987457833095",
	 		    scope: "",
			    authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
			    redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
			authFB.Completed += AuthFB_Completed;
			StartActivity(authFB.GetUI(this));
		}

		void TwitterLogin()
		{
			var auth = new OAuth1Authenticator(
				consumerKey: "jzK4V4eCkIhheAQwhykbwfJtB",
			    consumerSecret: "p0gT728V3kj8urYTHlZOTQbcTj7Y71eMUWnYCvQGHdrolzinQa",
		    	requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
			    authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
			    accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
			    callbackUrl: new Uri("http://mobile.twitter.com"));
			
			auth.Completed+= Auth_Completed;
			StartActivity(auth.GetUI(this));
		}

		void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
		{
			if (e.IsAuthenticated)
			{
				Toast.MakeText(this, "Twitter login Success", ToastLength.Short).Show();
			}
		}

		void AuthFB_Completed(object sender, AuthenticatorCompletedEventArgs e)
		{
			if (e.Account!=null)
			{
				Toast.MakeText(this, "Facebook login Success " + e.Account.Properties.Count, ToastLength.Short).Show();
			}
		}

		void AuthLinkedIn_Completed(object sender, AuthenticatorCompletedEventArgs e)
		{
			if (e.IsAuthenticated)
			{
				Toast.MakeText(this, "LinkedIn login Success", ToastLength.Short).Show();
			}
		}
	}
}

