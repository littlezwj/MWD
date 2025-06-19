using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TransitionRendererFeature : ScriptableRendererFeature
{
    public Shader shader;
    Material mat;
    TransitionPass pass;

    public override void Create()
    {
        if (shader == null) return;
        mat = CoreUtils.CreateEngineMaterial(shader);
        pass = new TransitionPass(mat)
        {
            renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData data)
    {
        if (mat == null) return;
        pass.Setup(renderer.cameraColorTargetHandle);
        renderer.EnqueuePass(pass);
    }
}
