//-----------------------------------------------------------------------
// <copyright file="PhraseTypes.cs" company="AVLozhechkin">
//     Copyright (c) AVLozhechkin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DashboardGame
{
    /// <summary>
    /// This enum contains all phrase types that can be used in game.
    /// </summary>
    internal enum PhraseTypes
    {
        /// <summary>
        /// Welcome phrase.
        /// </summary>
        Welcome,

        /// <summary>
        /// Introduction to options.
        /// </summary>
        Options,
        
        /// <summary>
        /// Option for plotting dot.
        /// </summary>
        DotOption,

        /// <summary>
        /// Option for plotting vertical line.
        /// </summary>
        VerLineOption,

        /// <summary>
        /// Option for plotting horizontal line.
        /// </summary>
        HorLineOption,

        /// <summary>
        /// Option for plotting parabola.
        /// </summary>
        ParOption,

        /// <summary>
        /// How to choose tip.
        /// </summary>
        HowToChoose,

        /// <summary>
        /// First part of example tip.
        /// </summary>
        Example1,

        /// <summary>
        /// Second part of example tip.
        /// </summary>
        Example2,

        /// <summary>
        /// Info about escape.
        /// </summary>
        Escape,

        /// <summary>
        /// Choose suggestion.
        /// </summary>
        Choose,

        /// <summary>
        /// Phrase for showing choice.
        /// </summary>
        ChoiceIs,

        /// <summary>
        /// Tip on how to plot.
        /// </summary>
        TypeToPlot,
        
        /// <summary>
        /// Empty input message.
        /// </summary>
        EmptyInput,

        /// <summary>
        /// First part of incorrect input message.
        /// </summary>
        IncorrectInput1,

        /// <summary>
        /// Second part of incorrect input message.
        /// </summary>
        IncorrectInput2
    }
}
