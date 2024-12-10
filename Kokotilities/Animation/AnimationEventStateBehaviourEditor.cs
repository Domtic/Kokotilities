using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

[CustomEditor(typeof(AnimationEventStateBehaviour))]
public class AnimationEventStateBehaviourEditor : Editor
{
    AnimationClip previewClip;
    float previewTime;
    bool isPreviewing;

    [MenuItem("GameObject/Enforce T-Pose", false, 0)]
    static void EnforceTpose()
    {
        GameObject selected = Selection.activeGameObject;
        if (!selected || !selected.TryGetComponent(out Animator animator) || !animator.avatarRoot)
            return;

        //TODO: add protection for bbjects that have no bones, and rather have positions
        if (animator.avatar == null)
            return;
        SkeletonBone[] skeletonBones = animator.avatar.humanDescription.skeleton;
        foreach(HumanBodyBones hbb in Enum.GetValues(typeof(HumanBodyBones)))
        {
            if (hbb == HumanBodyBones.LastBone)
                continue;

            Transform boneTransform = animator.GetBoneTransform(hbb);
            if (!boneTransform)
                continue;

            SkeletonBone skeletonBone = skeletonBones.FirstOrDefault(sb => sb.name == boneTransform.name);
            if (skeletonBone.name == null)
                continue;

            if (hbb == HumanBodyBones.Hips)
                boneTransform.localPosition = skeletonBone.position;

            boneTransform.localRotation = skeletonBone.rotation;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        AnimationEventStateBehaviour stateBehaviour = (AnimationEventStateBehaviour)target;
        if(Validate(stateBehaviour, out string errorMessage))
        {
            GUILayout.Space(10);
            if(isPreviewing)
            {
                if (GUILayout.Button("Stop preview"))
                {                 
                    isPreviewing = false;
                    EnforceTpose();
                }
                else
                {
                    PreviewAnimationClip(stateBehaviour);
                }
               
            }
            else if (GUILayout.Button("Preview"))
            {
                    isPreviewing = true;
            }

          
            GUILayout.Label("Previewing at " + previewTime + "s", EditorStyles.helpBox);
        }
        else
        {
            EditorGUILayout.HelpBox(errorMessage, MessageType.Info);
        }
    }

    private void PreviewAnimationClip(AnimationEventStateBehaviour stateBehaviour)
    {
        if (previewClip == false)
            return;


        previewTime = stateBehaviour.triggerTime * previewClip.length;

        AnimationMode.StartAnimationMode();
        AnimationMode.SampleAnimationClip(Selection.activeGameObject, previewClip, previewTime);
        AnimationMode.StopAnimationMode();
    }

    bool Validate(AnimationEventStateBehaviour stateBehaviour, out string errorMessage)
    {
        AnimatorController animatorController = GetValidAnimatorController(out errorMessage);
        if (animatorController == null)
            return false;

        ChildAnimatorState matchingState = animatorController.layers.SelectMany(layer => layer.stateMachine.states).FirstOrDefault(state => state.state.behaviours.Contains(stateBehaviour));

        previewClip = matchingState.state?.motion as AnimationClip;
        if(previewClip == null)
        {
            errorMessage = "No valid animationClip found for the curret state";
            return false;
        }

        return true;
    }
    

    AnimatorController GetValidAnimatorController(out string errorMessage)
    {
        errorMessage = string.Empty;
        GameObject targetGameObject = Selection.activeGameObject;
        if(targetGameObject == null)
        {
            errorMessage = "Please select a gameobject with an animatior to preview, Makes sure you lock the inspector so when you select an object, you don't remove the preview of this animation";
            return null;
        }

        Animator animator = targetGameObject.GetComponent<Animator>();
        if(animator == null)
        {
            errorMessage = "The selected game object does not have an animator component";
            return null;
        }

        AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
        if(animatorController == null)
        {
            errorMessage = "The selected Animator does not have a valid animator controller";
            return null;
        }

        return animatorController;
    }
}
