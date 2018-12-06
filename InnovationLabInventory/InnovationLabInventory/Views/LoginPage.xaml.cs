using System;
using System.Collections.Generic;
using Xamarin.Forms;
using InnovationLabInventory.WebServices;
using InnovationLabInventory.Views;
using Xamarin.Auth;
using Newtonsoft.Json;
using System.Linq;
using System.Diagnostics;

namespace InnovationLabInventory.Views
{
    public partial class LoginPage : ContentPage
    {
        CloudDataStore cloudStore = new CloudDataStore();
        public LoginPage()
        {
            InitializeComponent();
        }
        async void Login_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                if (emailTextField.Text == null &&
                    passwordTextField.Text == null)
                {
                    await DisplayAlert("Error","Please Enter User Name and Password","Cancel", "Ok");
                }
                else if (emailTextField.Text == null)
                {
                    await DisplayAlert("Error","Please Enter User Name", "Cancel", "Ok");
                }
                else if (passwordTextField.Text == null)
                {
                    await DisplayAlert("Error","Please Enter Password", "Cancel", "Ok");
                }
                else
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict["username"] = emailTextField.Text;
                    dict["password"] = passwordTextField.Text;
                    var isSuccess = await cloudStore.CheckLogin(dict);
                    if (isSuccess != null)
                    {
                        string User = isSuccess.userType.ToString();
                        if (User == "manager")
                        {
                            await Navigation.PushModalAsync(new MainPage());
                        }
                        else
                        {
                            await Navigation.PushModalAsync(new UserPage());
                        }

                    }
                    else
                    {
                        await DisplayAlert("Error", "Wrong Email or Password.", "Cancel", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                emailTextField.Text = string.Empty;
                passwordTextField.Text = string.Empty;
                await DisplayAlert("Error", "Wrong Email or Password.", "Cancel", "Ok");

            }
        }
    }
}

