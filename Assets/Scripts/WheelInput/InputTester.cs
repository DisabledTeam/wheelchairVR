using System.Text;
using TMPro;
using UnityEngine;

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
            var sb = new StringBuilder();

            sb.Append("firstButton.state:");
            sb.Append(InputConfiguration.Instance.firstButton.state);
            sb.Append("\n");

            sb.Append("firstButtonAnalog.axis:");
            sb.Append(InputConfiguration.Instance.firstButtonAnalog.axis);
            sb.Append("\n");

            sb.Append("secondButton.state:");
            sb.Append(InputConfiguration.Instance.secondButton.state);
            sb.Append("\n");

            sb.Append("joyStick.axis:");
            sb.Append(InputConfiguration.Instance.joyStick.axis);
            sb.Append("\n");


            sb.Append("joyStick.delta:");
            sb.Append(InputConfiguration.Instance.joyStick.delta);
            sb.Append("\n");


            sb.Append("joystickTouched.state:");
            sb.Append(InputConfiguration.Instance.joystickTouched.state);
            sb.Append("\n");

            return sb.ToString();
        }
    }
}