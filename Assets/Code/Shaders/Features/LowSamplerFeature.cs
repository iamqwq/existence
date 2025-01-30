using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Code.Shaders.Features {
    public class LowSamplerFeature : ScriptableRendererFeature {
        
        private LowSampleRenderPass _pass;
        
        public override void Create() {
            _pass = new LowSampleRenderPass(
                RenderPassEvent.AfterRenderingSkybox,
                new Material(Shader.Find("Project/PostProcessing/LowSample"))
            );
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
            _pass.Setup(renderingData);
            renderer.EnqueuePass(_pass);
        }

        public class LowSampleRenderPass : ScriptableRenderPass {
            
            private readonly Material _material;
            private RenderTextureDescriptor _colTempDesc;
            private RTHandle _colSrcHandle;
            private RTHandle _colTempHandle;

            public LowSampleRenderPass(RenderPassEvent renderPassEvent, Material material) {
                this.renderPassEvent = renderPassEvent;
                _material = material;
            }

            public void Setup(in RenderingData renderingData) {
                var desc = renderingData.cameraData.cameraTargetDescriptor;
                desc.depthBufferBits = 0;
                _colTempDesc = desc;
                RenderingUtils.ReAllocateIfNeeded(ref _colTempHandle, _colTempDesc, name: "_colTemp");
            }

            public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData) {
                RenderingUtils.ReAllocateIfNeeded(ref _colTempHandle, _colTempDesc, name: "_colTemp");
                _colSrcHandle = renderingData.cameraData.renderer.cameraColorTargetHandle;
            }


            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
                var cmd = CommandBufferPool.Get();
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();
                cmd.Blit(_colSrcHandle, _colTempHandle, _material);
                cmd.Blit(_colTempHandle, _colSrcHandle);
                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }

            public override void OnCameraCleanup(CommandBuffer cmd) {
                _colTempHandle.rt?.Release();
            }
        }
    }
}
