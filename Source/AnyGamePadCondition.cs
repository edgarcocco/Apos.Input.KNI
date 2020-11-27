using Microsoft.Xna.Framework.Input;

namespace Apos.Input {
    /// <summary>
    /// Checks various conditions on a specific gamepad button for all gamepads.
    /// Non static methods implicitly make sure that the game is active. Otherwise returns false.
    /// </summary>
    public class AnyGamePadCondition : ICondition {

        // Group: Constructors

        /// <param name="button">The button to operate on.</param>
        public AnyGamePadCondition(GamePadButton button) {
            _button = button;
        }

        // Group: Public Functions

        /// <returns>Returns true when the button was not pressed and is now pressed.</returns>
        public bool Pressed() {
            return Pressed(_button) && InputHelper.IsActive;
        }
        /// <returns>Returns true when the button is now pressed.</returns>
        public bool Held() {
            return Held(_button) && InputHelper.IsActive;
        }
        /// <returns>Returns true when the button was pressed and is now pressed.</returns>
        public bool HeldOnly() {
            return HeldOnly(_button) && InputHelper.IsActive;
        }
        /// <returns>Returns true when the button was pressed and is now not pressed.</returns>
        public bool Released() {
            return Released(_button) && InputHelper.IsActive;
        }

        // Group: Static Functions

        /// <returns>Returns true when the button was not pressed and is now pressed.</returns>
        public static bool Pressed(GamePadButton button) {
            for (int i = 0; i < GamePad.MaximumGamePadCount; i++) {
                if (InputHelper.GamePadButtons[button](InputHelper.NewGamePad, i) == ButtonState.Pressed &&
                    InputHelper.GamePadButtons[button](InputHelper.OldGamePad, i) == ButtonState.Released) {
                    return true;
                }
            }
            return false;
        }
        /// <returns>Returns true when the button is now pressed.</returns>
        public static bool Held(GamePadButton button) {
            for (int i = 0; i < GamePad.MaximumGamePadCount; i++) {
                if (InputHelper.GamePadButtons[button](InputHelper.NewGamePad, i) == ButtonState.Pressed) {
                    return true;
                }
            }
            return false;
        }
        /// <returns>Returns true when the button was pressed and is now pressed.</returns>
        public static bool HeldOnly(GamePadButton button) {
            for (int i = 0; i < GamePad.MaximumGamePadCount; i++) {
                if (InputHelper.GamePadButtons[button](InputHelper.NewGamePad, i) == ButtonState.Pressed &&
                    InputHelper.GamePadButtons[button](InputHelper.OldGamePad, i) == ButtonState.Pressed) {
                    return true;
                }
            }
            return false;
        }
        /// <returns>Returns true when the button was pressed and is now not pressed.</returns>
        public static bool Released(GamePadButton button) {
            for (int i = 0; i < GamePad.MaximumGamePadCount; i++) {
                if (InputHelper.GamePadButtons[button](InputHelper.NewGamePad, i) == ButtonState.Released &&
                    InputHelper.GamePadButtons[button](InputHelper.OldGamePad, i) == ButtonState.Pressed) {
                    return true;
                }
            }
            return false;
        }

        // Group: Private Variables

        /// <summary>
        /// The button that will be checked.
        /// </summary>
        private GamePadButton _button;
    }
}
