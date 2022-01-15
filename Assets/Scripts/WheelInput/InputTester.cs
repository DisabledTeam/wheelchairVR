using System.Text;
using TMPro;
using UnityEngine;
using Valve.VR;

namespace WheelInput
{
    public class InputTester : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fullInputText;

        private void Update()
        {
            fullInputText.text = GetInputValuesText();
        }

        private string GetInputValuesText()
        {
            var ic = InputConfiguration.Instance;
            var sb = new StringBuilder();

            sb.Append("firstButton.state:");
            sb.Append(ic.firstButton.GetState(SteamVR_Input_Sources.Any));
            sb.Append(", ");
            sb.Append(ic.firstButton.GetState(SteamVR_Input_Sources.LeftHand));
            sb.Append(", ");
            sb.Append(ic.firstButton.GetState(SteamVR_Input_Sources.RightHand));
            sb.Append("\n");

            sb.Append("firstButtonAnalog.axis:");
            sb.Append(ic.firstButtonAnalog.GetAxis(SteamVR_Input_Sources.Any));
            sb.Append(", ");
            sb.Append(ic.firstButtonAnalog.GetAxis(SteamVR_Input_Sources.LeftHand));
            sb.Append(", ");
            sb.Append(ic.firstButtonAnalog.GetAxis(SteamVR_Input_Sources.RightHand));
            sb.Append("\n");

            sb.Append("secondButton.state:");
            sb.Append(ic.secondButton.GetState(SteamVR_Input_Sources.Any));
            sb.Append(", ");
            sb.Append(ic.secondButton.GetState(SteamVR_Input_Sources.LeftHand));
            sb.Append(", ");
            sb.Append(ic.secondButton.GetState(SteamVR_Input_Sources.RightHand));
            sb.Append("\n");

            sb.Append("joyStick.axis:");
            sb.Append(ic.joyStick.axis);
            sb.Append("\n");


            sb.Append("joyStick.delta:");
            sb.Append(ic.joyStick.delta);
            sb.Append("\n");


            sb.Append("joystickTouched.state:");
            sb.Append(ic.joystickTouched.GetState(SteamVR_Input_Sources.Any));
            sb.Append(", ");
            sb.Append(ic.joystickTouched.GetState(SteamVR_Input_Sources.LeftHand));
            sb.Append(", ");
            sb.Append(ic.joystickTouched.GetState(SteamVR_Input_Sources.RightHand));
            sb.Append("\n");

            return sb.ToString();
        }
    }
}