using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TransitionPass : ScriptableRenderPass
{
    Material mat;
    RTHandle inputHandle;
    RTHandle tempHandle;

    public TransitionPass(Material m)
    {
        mat = m;
    }

    public void Setup(RTHandle src)
    {
        inputHandle = src;
    }

    public override void Configure(CommandBuffer cmd, RenderTextureDescriptor camDesc)
    {
        // 创建目标 RTHandle 区域
        RenderingUtils.ReAllocateIfNeeded(
            ref tempHandle, camDesc, FilterMode.Bilinear, TextureWrapMode.Clamp, name: "_TempTarget");
        ConfigureTarget(tempHandle);
    }

    public override void Execute(ScriptableRenderContext ctx, ref RenderingData data)
    {
        if (mat == null || inputHandle == null) return;

        var cmd = CommandBufferPool.Get("TransitionPass");
        // 先从 input → temp
        Blitter.BlitCameraTexture(cmd, inputHandle, tempHandle, mat, 0);
        // 然后再从 temp → input
        Blitter.BlitCameraTexture(cmd, tempHandle, inputHandle, mat, 0);
        ctx.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}
