#region Includes
using AUIT.AdaptationObjectives.Definitions;
using AUIT.ContextSources;
using AUIT.Extras.Datastructures;
using UnityEngine;
#endregion
namespace AUIT.AdaptationObjectives.Objectives
{
    /// <summary>
    /// 
    /// </summary>
    public class ImportanceControl : LocalObjective
    {
        #region Variables
        private float normalizedImportance;
        #endregion

        public override ObjectiveType ObjectiveType => ObjectiveType.Importance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optimizationTarget"></param>
        /// <param name="initialLayout"></param>
        /// 
        public override float CostFunction(Layout optimizationTarget, Layout initialLayout = null)
        {
            float normalizedImportance = 1f/(1f + Mathf.Exp(-GetComponent<WindowGazeData>().growthSpeed * GetComponent<WindowGazeData>().importance)*(1f/GetComponent<WindowGazeData>().lowerBoundAlpha - 1f));
            float alpha = optimizationTarget.Alpha;
            float importanceCost = Mathf.Abs(normalizedImportance - alpha);
            float focusCost = (GetComponent<WindowGazeData>().gazeStay == true) ? 1f - alpha : 1f;
            return Mathf.Min(importanceCost, focusCost);
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optimizationTarget"></param>
        /// <param name="initialLayout"></param>

        public override Layout OptimizationRule(Layout optimizationTarget, Layout initialLayout)
        {
            Layout result = optimizationTarget.Clone();
            float xyscale = GetComponent<WindowGazeData>().windowScale;
            if (GetComponent<WindowGazeData>().gazeStay == true)
            {
                result.Alpha = 1f;
                result.Scale = new Vector3(xyscale,xyscale,1f);
            }
            else
            {
                float normalizedImportance = 1f/(1f + Mathf.Exp(-GetComponent<WindowGazeData>().growthSpeed * GetComponent<WindowGazeData>().importance)*(1f/GetComponent<WindowGazeData>().lowerBoundAlpha - 1f));
                result.Alpha = normalizedImportance;
                float rescale = Mathf.Pow(normalizedImportance, GetComponent<WindowGazeData>().rescaleCoefficient) * xyscale;
                result.Scale = new Vector3(rescale, rescale, 1f);
            }
            return result;
        }
        
        public override Layout DirectRule(Layout optimizationTarget)
        {
            throw new System.NotImplementedException();
        }

        public override float[] GetParameters()
        {
            throw new System.NotImplementedException();
        }

        public override void SetParameters(float[] parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}