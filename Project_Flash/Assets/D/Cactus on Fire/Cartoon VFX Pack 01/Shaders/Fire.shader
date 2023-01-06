// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1,x:34275,y:32598,varname:node_1,prsc:2|custl-255-OUT,alpha-284-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:32968,y:32561,varname:node_1990,prsc:2,tex:b60c6853b7f034d46a9f5a892c2d240d,ntxv:0,isnm:False|UVIN-6491-UVOUT,TEX-5048-TEX;n:type:ShaderForge.SFN_Tex2d,id:3,x:32798,y:32962,ptovrint:False,ptlb:Cellular,ptin:_Cellular,varname:node_8025,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-16-UVOUT;n:type:ShaderForge.SFN_Lerp,id:4,x:33222,y:32807,varname:node_4,prsc:2|A-2552-OUT,B-222-OUT,T-1495-OUT;n:type:ShaderForge.SFN_RemapRange,id:9,x:33430,y:32724,varname:node_9,prsc:2,frmn:0,frmx:1,tomn:-400,tomx:300|IN-4-OUT;n:type:ShaderForge.SFN_Panner,id:16,x:32636,y:32962,varname:node_16,prsc:2,spu:-0.3,spv:-1|UVIN-20-UVOUT,DIST-42-OUT;n:type:ShaderForge.SFN_Time,id:19,x:32231,y:33220,varname:node_19,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:20,x:32414,y:32837,varname:node_20,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Lerp,id:42,x:32441,y:33195,varname:node_42,prsc:2|A-6551-OUT,B-19-T,T-87-OUT;n:type:ShaderForge.SFN_RemapRange,id:65,x:33430,y:32888,varname:node_65,prsc:2,frmn:0,frmx:1,tomn:-550,tomx:300|IN-4-OUT;n:type:ShaderForge.SFN_Clamp01,id:68,x:33598,y:32724,varname:node_68,prsc:2|IN-9-OUT;n:type:ShaderForge.SFN_Clamp01,id:70,x:33598,y:32888,varname:node_70,prsc:2|IN-65-OUT;n:type:ShaderForge.SFN_Color,id:77,x:33774,y:32397,ptovrint:False,ptlb:Flame Color,ptin:_FlameColor,varname:node_1451,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8823529,c2:0.6785885,c3:0.2400519,c4:1;n:type:ShaderForge.SFN_Slider,id:87,x:32239,y:33413,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_2712,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8717949,max:5;n:type:ShaderForge.SFN_Lerp,id:255,x:33774,y:32724,varname:node_255,prsc:2|A-77-RGB,B-258-RGB,T-68-OUT;n:type:ShaderForge.SFN_Color,id:258,x:33774,y:32568,ptovrint:False,ptlb:Flame Outline,ptin:_FlameOutline,varname:node_9544,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_OneMinus,id:284,x:33774,y:32888,varname:node_284,prsc:2|IN-70-OUT;n:type:ShaderForge.SFN_OneMinus,id:2552,x:33222,y:32681,varname:node_2552,prsc:2|IN-2289-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:5048,x:32386,y:32487,ptovrint:False,ptlb:Fire Shape,ptin:_FireShape,varname:node_5048,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b60c6853b7f034d46a9f5a892c2d240d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9054,x:32968,y:32420,varname:node_9054,prsc:2,tex:b60c6853b7f034d46a9f5a892c2d240d,ntxv:0,isnm:False|UVIN-4573-OUT,MIP-6646-OUT,TEX-5048-TEX;n:type:ShaderForge.SFN_TexCoord,id:6491,x:32790,y:32561,varname:node_6491,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:4573,x:32790,y:32420,varname:node_4573,prsc:2|A-6491-UVOUT,B-8364-OUT;n:type:ShaderForge.SFN_Vector2,id:8364,x:32607,y:32460,varname:node_8364,prsc:2,v1:0,v2:-0.01;n:type:ShaderForge.SFN_Tex2d,id:9563,x:32968,y:32271,varname:node_9563,prsc:2,tex:b60c6853b7f034d46a9f5a892c2d240d,ntxv:0,isnm:False|UVIN-8974-OUT,MIP-2795-OUT,TEX-5048-TEX;n:type:ShaderForge.SFN_Add,id:8974,x:32790,y:32271,varname:node_8974,prsc:2|A-6491-UVOUT,B-2163-OUT;n:type:ShaderForge.SFN_Vector2,id:2163,x:32607,y:32311,varname:node_2163,prsc:2,v1:0,v2:-0.05;n:type:ShaderForge.SFN_Vector1,id:6646,x:32386,y:32394,varname:node_6646,prsc:2,v1:3;n:type:ShaderForge.SFN_Vector1,id:2795,x:32386,y:32311,varname:node_2795,prsc:2,v1:4;n:type:ShaderForge.SFN_Add,id:1491,x:33226,y:32412,varname:node_1491,prsc:2|A-2-R,B-9054-R,C-9563-R,D-6861-R;n:type:ShaderForge.SFN_RemapRange,id:8299,x:33390,y:32412,varname:node_8299,prsc:2,frmn:0,frmx:1,tomn:0,tomx:0.3|IN-1491-OUT;n:type:ShaderForge.SFN_Clamp01,id:2289,x:33567,y:32412,varname:node_2289,prsc:2|IN-8299-OUT;n:type:ShaderForge.SFN_Vector1,id:6551,x:32231,y:33162,varname:node_6551,prsc:2,v1:0;n:type:ShaderForge.SFN_Tex2d,id:6861,x:32968,y:32112,varname:node_6861,prsc:2,tex:b60c6853b7f034d46a9f5a892c2d240d,ntxv:0,isnm:False|UVIN-2375-OUT,MIP-9786-OUT,TEX-5048-TEX;n:type:ShaderForge.SFN_Add,id:2375,x:32790,y:32112,varname:node_2375,prsc:2|A-6491-UVOUT,B-581-OUT;n:type:ShaderForge.SFN_Vector2,id:581,x:32607,y:32152,varname:node_581,prsc:2,v1:0,v2:-0.1;n:type:ShaderForge.SFN_Vector1,id:9786,x:32386,y:32226,varname:node_9786,prsc:2,v1:6;n:type:ShaderForge.SFN_Vector1,id:1495,x:33222,y:32950,varname:node_1495,prsc:2,v1:0.35;n:type:ShaderForge.SFN_OneMinus,id:222,x:32986,y:32962,varname:node_222,prsc:2|IN-3-R;proporder:87-77-258-5048-3;pass:END;sub:END;*/

Shader "Shader Forge/Fire" {
    Properties {
        _Speed ("Speed", Range(0, 5)) = 0.8717949
        _FlameColor ("Flame Color", Color) = (0.8823529,0.6785885,0.2400519,1)
        _FlameOutline ("Flame Outline", Color) = (0,0,0,1)
        _FireShape ("Fire Shape", 2D) = "white" {}
        _Cellular ("Cellular", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _Cellular; uniform float4 _Cellular_ST;
            uniform float4 _FlameColor;
            uniform float _Speed;
            uniform float4 _FlameOutline;
            uniform sampler2D _FireShape; uniform float4 _FireShape_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float4 node_1990 = tex2D(_FireShape,TRANSFORM_TEX(i.uv0, _FireShape));
                float2 node_4573 = (i.uv0+float2(0,-0.01));
                float4 node_9054 = tex2Dlod(_FireShape,float4(TRANSFORM_TEX(node_4573, _FireShape),0.0,3.0));
                float2 node_8974 = (i.uv0+float2(0,-0.05));
                float4 node_9563 = tex2Dlod(_FireShape,float4(TRANSFORM_TEX(node_8974, _FireShape),0.0,4.0));
                float2 node_2375 = (i.uv0+float2(0,-0.1));
                float4 node_6861 = tex2Dlod(_FireShape,float4(TRANSFORM_TEX(node_2375, _FireShape),0.0,6.0));
                float4 node_19 = _Time;
                float2 node_16 = (i.uv0+lerp(0.0,node_19.g,_Speed)*float2(-0.3,-1));
                float4 _Cellular_var = tex2D(_Cellular,TRANSFORM_TEX(node_16, _Cellular));
                float node_4 = lerp((1.0 - saturate(((node_1990.r+node_9054.r+node_9563.r+node_6861.r)*0.3+0.0))),(1.0 - _Cellular_var.r),0.35);
                float3 finalColor = lerp(_FlameColor.rgb,_FlameOutline.rgb,saturate((node_4*700.0+-400.0)));
                fixed4 finalRGBA = fixed4(finalColor,(1.0 - saturate((node_4*850.0+-550.0))));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
