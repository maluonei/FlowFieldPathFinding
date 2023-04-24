Shader "Custom/BNULOD" {
	SubShader{							//使物体渲染为红色的SubShader
	  LOD 600									//设置LOD数值为600
	  CGPROGRAM
	  #pragma surface surf Lambert						//表面着色器编译指令
	  struct Input {									//Input结构体
		float2 uv_MainTex;
	  };
	  void surf(Input IN, inout SurfaceOutput o) {			//表面着色器函数
		o.Albedo = float3(1,1,1);							//设置颜色为白色
	  }
	  ENDCG
	}
		SubShader{							//使物体渲染为绿色的SubShader
		  LOD 500										//设置LOD数值为500
		  CGPROGRAM
		  #pragma surface surf Lambert						//表面着色器编译指令
		  struct Input {									//Input结构体
			float2 uv_MainTex;
		  };
		  void surf(Input IN, inout SurfaceOutput o) {			//表面着色器函数
			o.Albedo = float3(1,0,0);							//设置颜色为红色
		  }
		  ENDCG
	  }
		  SubShader{							//使物体渲染为蓝色的SubShader
			LOD 400										//设置LOD数值为400
			CGPROGRAM
			#pragma surface surf Lambert						//表面着色器编译指令
			struct Input {									//Input结构体
			  float2 uv_MainTex;
			};
			void surf(Input IN, inout SurfaceOutput o) {			//表面着色器函数
			  o.Albedo = float3(0,0,1);							//设置颜色为蓝色
			}
			ENDCG
		  }
			  FallBack "Diffuse"
}
