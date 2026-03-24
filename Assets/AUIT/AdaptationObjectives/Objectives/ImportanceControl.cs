using AUIT.AdaptationObjectives.Definitions;
using AUIT.ContextSources;
using AUIT.Extras.Datastructures;
using UnityEngine;

namespace AUIT.AdaptationObjectives.Objectives
{
    public class ImportanceControl : LocalObjective
    {
        private float normalizedImportance;


        public override ObjectiveType ObjectiveType => ObjectiveType.NotSpecified;
        public override float CostFunction(Layout optimizationTarget, Layout initialLayout = null)
        {
            float normalizedImportance = 1f/(1f + Mathf.Exp(-0.3f * GetComponent<WindowGazeData>().importance)*(1f/0.1f - 1f));
            float alpha = optimizationTarget.Alpha;
            float importanceCost = Mathf.Abs(normalizedImportance - alpha);
            float focusCost = (GetComponent<WindowGazeData>().gazeStay == true) ? 1f - alpha : 1f;
            Debug.Log("Importance Cost: " + importanceCost);
            Debug.Log("Focus Cost: " + focusCost);
            return Mathf.Min(importanceCost, focusCost);
        }
    

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
                float normalizedImportance = 1f/(1f + Mathf.Exp(-0.3f * GetComponent<WindowGazeData>().importance)*(1f/0.1f - 1f));
                result.Alpha = normalizedImportance;
                Debug.Log("Resulting Alpha: " + result.Alpha);
                float rescale = Mathf.Pow(normalizedImportance, 0.05f) * xyscale;
                Debug.Log("Window Scale: " + xyscale + ", and Rescale Value: " + rescale);
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