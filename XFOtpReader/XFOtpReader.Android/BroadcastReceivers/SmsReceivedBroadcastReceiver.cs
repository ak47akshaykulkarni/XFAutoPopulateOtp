using System.Text.RegularExpressions;
using Android.Content;
using Android.Runtime;
using Android.Telephony;
using Xamarin.Forms;

namespace XFOtpReader.Droid.BroadcastReceivers
{
    [BroadcastReceiver(Enabled = true, Label = "Otp Message Receiver")]
    public class SmsReceivedBroadcastReceiver: BroadcastReceiver
    {
        public static readonly string IntentAction = "android.provider.Telephony.SMS_RECEIVED";
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action != IntentAction) return;

            var bundle = intent.Extras;

            if (bundle == null) return;

            var pdus = bundle.Get("pdus");

            var castedPdus = JNIEnv.GetArray<IJavaObject>(pdus.Handle);
            var msgs = new SmsMessage[castedPdus.Length];
            
            for (var i = 0; i < msgs.Length; i++)
            {
                var bytes = new byte[JNIEnv.GetArrayLength(castedPdus[i].Handle)];
                JNIEnv.CopyArray(castedPdus[i].Handle, bytes);

                msgs[i] = SmsMessage.CreateFromPdu(bytes);
                string messageBody = "";
                messageBody += msgs[i].MessageBody;
                var otpValue = Regex.Match(messageBody, @"\d{4,6}").Value;
                MessagingCenter.Send<string,string>("OtpMessage", "NotifyMsg", otpValue);
            }
        }
    }
}