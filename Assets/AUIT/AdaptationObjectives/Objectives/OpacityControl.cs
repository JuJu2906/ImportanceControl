using AUIT.AdaptationObjectives.Definitions;
using AUIT.ContextSources;
using AUIT.Extras.Datastructures;
using UnityEngine;

namespace AUIT.AdaptationObjectives.Objectives
{
    public class OpacityControl : LocalObjective
    {
        private float normalizedImportance;


        public override ObjectiveType ObjectiveType => ObjectiveType.NotSpecified;
        public override float CostFunction(Layout optimizationTarget, Layout initialLayout = null)
        {
            float normalizedImportance = 1f/(1f + Mathf.Exp(-0.5f * GetComponent<WindowGazeData>().importance)*(1f/0.1f - 1f));
            float alpha = optimizationTarget.Alpha;
            float importanceCost = Mathf.Pow(normalizedImportance - alpha, 2);
            float focusCost = (GetComponent<WindowGazeData>().gazeStay == true) ? 1f - alpha : 1f;
            Debug.Log("Importance Cost: " + importanceCost);
            Debug.Log("Focus Cost: " + focusCost);
            return Mathf.Min(importanceCost, focusCost);
        }
    

        public override Layout OptimizationRule(Layout optimizationTarget, Layout initialLayout)
        {
            float normalizedImportance = 1f/(1f + Mathf.Exp(-0.5f * GetComponent<WindowGazeData>().importance)*(1f/0.1f - 1f));
            Layout result = optimizationTarget.CloneAlpha();
            result.Alpha = normalizedImportance;
            Debug.Log("Resulting Alpha: " + result.Alpha);
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