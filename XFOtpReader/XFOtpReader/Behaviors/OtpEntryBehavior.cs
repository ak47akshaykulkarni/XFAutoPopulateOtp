using System;
using System.Linq;
using System.Text.RegularExpressions;
using Unity;
using Xamarin.Forms;

namespace XFOtpReader.Behaviors
{
    public class OtpEntryBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            if (entry.Text is null || string.IsNullOrWhiteSpace(entry.Text))
            {
                var selectedClass = int.Parse(entry.ClassId);
                if (selectedClass == 1)
                {
                    return;
                }
                var prevClassId = selectedClass - 1;
                var prevEntry = (Entry)((Grid)entry.Parent).Children.FirstOrDefault(x => x.ClassId == prevClassId.ToString());
                if (prevEntry == null || prevEntry.GetType() != typeof(Entry)) return;
                prevEntry.Focus();
                return;
            }
            int intValue;
            bool isInt = int.TryParse(entry.Text, out intValue);
            if (!isInt)
            {
                entry.TextChanged -= OnEntryTextChanged;
                entry.Text = string.Empty;
                entry.TextChanged += OnEntryTextChanged;
                return;
            }
            entry.TextChanged -= OnEntryTextChanged;
            entry.Text = intValue.ToString();
            var selectedClassId = int.Parse(entry.ClassId);
            if (selectedClassId == 4)
            {
                entry.Unfocus();
                return;
            }
            var nextClassId = selectedClassId + 1;
            var parentGrid =(Grid) entry.Parent;
            var nextEntry = (Entry)parentGrid.Children.FirstOrDefault(x => x.ClassId == nextClassId.ToString());
            if (nextEntry == null || nextEntry.GetType() !=typeof(Entry)) return;
            entry.Unfocus();
            nextEntry.Focus();
            entry.TextChanged += OnEntryTextChanged;
        }
    }
}
