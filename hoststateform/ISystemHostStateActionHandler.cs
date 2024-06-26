// Id:        ISystemHostStateActionHandler.cs
//
// Function:  IAR Visual State API system action handler interface.
//
// This is an automatically generated file. It will be overwritten by the Coder.
//
// DO NOT EDIT THE FILE!



//
// SEM Variable Types.
//
using SEM_EVENT_TYPE = global::System.Byte;


public interface ISystemHostStateActionHandler
{

  void appInit();

  void pauseHost();

  void restoreHost();

  byte startHost();

  void stopHost();

} // end of ISystemHostStateActionHandler class
