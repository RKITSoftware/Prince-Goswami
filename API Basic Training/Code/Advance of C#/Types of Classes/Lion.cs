

namespace TypesOfClass
{
    /// <summary>
    /// This is a sealed class representing a Lion in the zoo.
    /// </summary>
    public sealed class Lion : AbstractAnimal
    {
        /// <summary>
        /// Gets the sound of the lion.
        /// </summary>
        /// <returns>The sound of the lion.</returns>
        public override string GetSound()
        {
            return "Roar";
        }
    }

}
