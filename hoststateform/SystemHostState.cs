// Id:        SystemHostState.cs
//
// Function:  Contains all API functions.
//
// This is an automatically generated file. It will be overwritten by the Coder.
//
// DO NOT EDIT THE FILE!


//
// SEM Variable Types.
//
using SEM_EVENT_TYPE = global::System.Byte;
using SEM_EXPLANATION_TYPE = global::System.Byte;
using SEM_STATE_TYPE = global::System.Byte;
using SEM_STATE_MACHINE_TYPE = global::System.Byte;
using SEM_INSTANCE_TYPE = global::System.Byte;
using SEM_SIGNAL_QUEUE_TYPE = global::System.Byte;
using SEM_EVENT_GROUP_TYPE = global::System.Byte;


class SystemHostState
{
  ISystemHostStateActionHandler mHandler;


  //
  // Constructor initializing action handler.
  //
  public SystemHostState (ISystemHostStateActionHandler handler)
  {
    mHandler = handler;
  }


  //
  // Undefined State.
  //
  const byte STATE_UNDEFINED = 255;


  //
  // Undefined Event.
  //
  const byte EVENT_UNDEFINED = 255;


  //
  // Undefined Event Group.
  //
  const byte EVENT_GROUP_UNDEFINED = 255;


  //
  // Event Termination ID.
  //
  const byte EVENT_TERMINATION_ID = 255;


  //
  // Event Identifier Definitions.
  //
  public const byte SE_RESET = 0;
  public const byte clickPause = 1;
  public const byte clickRestore = 2;
  public const byte clickStart = 3;
  public const byte clickStop = 4;


  //
  // State Identifier Definitions.
  //
  const byte TopLevelStateMachineHostState_Region1_Init = 0;
  const byte TopLevelStateMachineHostState_Region1_Running = 1;
  const byte TopLevelStateMachineHostState_Region1_Pause = 2;


  //
  // State Machine Identifier Definitions.
  //
  const byte M_TopLevelStateMachineHostState_Region1 = 0;


  SEM_STATE_TYPE[] SystemHostStateCSV = new SEM_STATE_TYPE[1];


  public VSResult VSDeduct (SEM_EVENT_TYPE EventNo)
  {

    // scoped for events
    {
      SEM_STATE_TYPE[] WSV =
      {
        STATE_UNDEFINED
      };

      switch (EventNo)
      {
      case SE_RESET:
        {
          appInit();
          WSV[M_TopLevelStateMachineHostState_Region1/*0*/] = TopLevelStateMachineHostState_Region1_Init;
        }
        break;

      case clickPause:
        {
          if (SystemHostStateCSV[M_TopLevelStateMachineHostState_Region1/*0*/] == TopLevelStateMachineHostState_Region1_Running)
          {
            pauseHost();
            WSV[M_TopLevelStateMachineHostState_Region1/*0*/] = TopLevelStateMachineHostState_Region1_Pause;
          }
        }
        break;

      case clickRestore:
        {
          if (SystemHostStateCSV[M_TopLevelStateMachineHostState_Region1/*0*/] == TopLevelStateMachineHostState_Region1_Pause)
          {
            restoreHost();
            WSV[M_TopLevelStateMachineHostState_Region1/*0*/] = TopLevelStateMachineHostState_Region1_Running;
          }
        }
        break;

      case clickStart:
        {
          if (SystemHostStateCSV[M_TopLevelStateMachineHostState_Region1/*0*/] == TopLevelStateMachineHostState_Region1_Init)
          {
            startHost();
            WSV[M_TopLevelStateMachineHostState_Region1/*0*/] = TopLevelStateMachineHostState_Region1_Running;
          }
        }
        break;

      case clickStop:
        {
          if (SystemHostStateCSV[M_TopLevelStateMachineHostState_Region1/*0*/] == TopLevelStateMachineHostState_Region1_Running)
          {
            stopHost();
            WSV[M_TopLevelStateMachineHostState_Region1/*0*/] = TopLevelStateMachineHostState_Region1_Init;
          }
          else if (SystemHostStateCSV[M_TopLevelStateMachineHostState_Region1/*0*/] == TopLevelStateMachineHostState_Region1_Pause)
          {
            stopHost();
            WSV[M_TopLevelStateMachineHostState_Region1/*0*/] = TopLevelStateMachineHostState_Region1_Init;
          }
        }
        break;

      default:
        return VSResult.SES_RANGE_ERR;
      }

      /* Change the state vector */
      {
        uint i;
        for (i = 0; i < 1; ++i)
        {
          if (WSV[i] != STATE_UNDEFINED)
          {
            SystemHostStateCSV[i] = WSV[i];
          }
        }
      }
    }

    return VSResult.SES_OKAY;
  }


  public static VSResult VSElementExpl (IdentifierType IdentType, SEM_EXPLANATION_TYPE IdentNo, out string Text)
  {
    VSResult ret = VSResult.SES_OKAY;
    Text = "";
    switch (IdentType)
    {
    default:
      ret = VSResult.SES_TYPE_ERR;
      break;
    }
    return ret;
  }


  public static VSResult VSElementName (IdentifierType IdentType, SEM_EXPLANATION_TYPE IdentNo, out string Text)
  {
    VSResult ret = VSResult.SES_OKAY;
    Text = "";
    switch (IdentType)
    {
    default:
      ret = VSResult.SES_TYPE_ERR;
      break;
    }
    return ret;
  }


  public void VSInitAll ()
  {
    SEM_STATE_MACHINE_TYPE i;
    for (i = 0; i < 1; ++i)
    {
      SystemHostStateCSV[i] = STATE_UNDEFINED;
    }
    VSInitExternalVariables();
    VSInitInternalVariables();
  }


  public void VSInitExternalVariables ()
  {
  }


  public void VSInitInternalVariables ()
  {
  }


  //
  // Action handler forwarding methods.
  //

  void appInit()
  {
    mHandler.appInit();
  }

  void pauseHost()
  {
    mHandler.pauseHost();
  }

  void restoreHost()
  {
    mHandler.restoreHost();
  }

  byte startHost()
  {
    return mHandler.startHost();
  }

  void stopHost()
  {
    mHandler.stopHost();
  }

} // end of SystemHostState class
