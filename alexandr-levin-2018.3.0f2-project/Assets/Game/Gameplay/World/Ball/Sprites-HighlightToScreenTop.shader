// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Sprites Custom/Highlight To Screen Top"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _ScreenTopGradientColor ("Screen Top Gradient Color", Color) = (1,1,1,1)
        _ScreenTopGradientAmount ("Screen Top Gradient Amount", Float) = 1
        _ScreenTopGradientOffset ("Screen Top Gradient Offset", Float) = 1
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        //Blend One OneMinusSrcAlpha
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex OverrideSpriteVert
            #pragma fragment OverrideSpriteFrag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"
            
            fixed4 _ScreenTopGradientColor;
            float _ScreenTopGradientAmount;
            float _ScreenTopGradientOffset;
            
            v2f OverrideSpriteVert(appdata_t IN)
            {
                v2f OUT = SpriteVert(IN);
            
                return OUT;
            }
            
            fixed4 OverrideSpriteFrag(v2f IN) : SV_Target
            {
                fixed4 OUT;
            
                fixed4 sourceColor = SpriteFrag(IN);
                
                float gradientProgress = IN.vertex.y / _ScreenParams.y;
                
                // handle flipped projection case
                if (_ProjectionParams.x > 0)
                    gradientProgress = 1 - gradientProgress;
                
                gradientProgress += _ScreenTopGradientOffset;
                
                OUT = lerp(sourceColor, _ScreenTopGradientColor, gradientProgress * _ScreenTopGradientAmount);
                OUT.a = sourceColor.a;
                
                return OUT;
            }
            
            ENDCG
        }
    }
}
