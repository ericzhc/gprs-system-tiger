using System;
using System.Windows.Forms;
using Tiger.Helper;

namespace Tiger
{
  /// <summary>
  /// This class contains logic and data the LoginTest application
  /// </summary>
  public class LoginContext: ApplicationContext
  {

    #region Private fields
    // here we can declare the all forms application and manage it directly
    //(show, close, set as MainForm and so on) 
    private FLogin _fLogin;
    private FMain _fMain;

    #endregion

    #region Initialization

    public LoginContext()
    {
      //CreateSplashForm();

      CreateLoginForm();

    }

    #endregion

    #region Private Methods

      /// <summary>
    /// The Login form
    /// initialization and show
    /// </summary>
    private void CreateLoginForm()
    {

      _fLogin = new FLogin();
      _fLogin.Closed += fLogin_Closed;
      MainForm = _fLogin;
      _fLogin.Show();

    }

    #endregion

    #region Events

    /// <summary>
    /// If the login procedure done successfully
    /// we'll see the Main Form
    /// else the application will close 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void fLogin_Closed( object sender, EventArgs e )
    {
        if (Loginwork.Logged) //if the user is logged
      {
        _fMain = new FMain();
        MainForm = _fMain; //set the main message loop applicaton in this form
        _fMain.Show();
      }
      else
      {
        ExitThread();
      }

    }

    #endregion
  }
}
