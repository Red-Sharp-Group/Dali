namespace RedSharp.Dali.Common.Enums
{
    /// <summary>
    /// Defines priority of action for executions.
    /// </summary>
    public enum DaliDispatcherPriorityEnum
    {
        /// <summary>
        /// Action will not be executed.
        /// </summary>
        Canceled,

        /// <summary>
        /// Action will be executed after all other background action.
        /// </summary>
        Idle,

        /// <summary>
        /// Action will be executed in background.
        /// </summary>
        Background,

        /// <summary>
        /// Action will be executed in the priority with rendering.
        /// </summary>
        Render,

        /// <summary>
        /// Action will be executed as soon as will be opportunity.
        /// </summary>
        Foreground
    }
}
