using ClientApp.Animations;
using ClientApp.ViewModels.ChatPage;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace ClientApp.Pages
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    //public partial class ChatPage : BasePage<ChatViewModel, Guid>
    public partial class ChatPage : BasePage<ChatViewModel>
    {
        public ChatPage() : base()
        {
            InitializeComponent();
        }

        //public ChatPage(ChatViewModel specificViewModel, Guid parameter) : base(specificViewModel, parameter)
        public ChatPage(ChatViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        protected override void OnViewModelChanged()
        {
            // Make sure UI exists first
            if (ChatMessageList == null)
                return;

            // Fade in chat message list
            var storyboard = new Storyboard();
            storyboard.AddFadeIn(1, hasFromValue: true);
            storyboard.Begin(ChatMessageList);

            // Make the message box focused
            MessageText.Focus();
        }

        /// <summary>
        /// Preview the input into the message box and respond as required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageText_PreviewKeyDown(Object sender, KeyEventArgs e)
        {
            // Get the text box
            var textbox = sender as TextBox;

            // Check if we have pressed enter
            if (e.Key == Key.Enter)
            {
                // If we have control pressed...
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    // Add a new line at the point where the cursor is
                    var index = textbox.CaretIndex;

                    // Insert the new line
                    textbox.Text = textbox.Text.Insert(index, Environment.NewLine);

                    // Shift the caret forward to the newline
                    textbox.CaretIndex = index + Environment.NewLine.Length;

                    // Mark this key as handled by us
                    e.Handled = true;
                }
                //else
                //{
                //    // Send the message
                //    ViewModel.Send();
                //}

                // Mark the key as handled
                //e.Handled = true;
            }
        }
    }
}
