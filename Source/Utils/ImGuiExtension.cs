using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace UImGui
{
	internal static unsafe class ImGuiExtension
	{
		private static readonly HashSet<IntPtr> _managedAllocations = new HashSet<IntPtr>();

		internal static void SetBackendPlatformName(this ImGuiIOPtr io, string name)
		{
			if (io.NativePtr->BackendPlatformName != (byte*)0)
			{
				if (_managedAllocations.Contains((IntPtr)io.NativePtr->BackendPlatformName))
				{
					Marshal.FreeHGlobal(new IntPtr(io.NativePtr->BackendPlatformName));
				}
				io.NativePtr->BackendPlatformName = (byte*)0;
			}
			if (name != null)
			{
				int byteCount = Encoding.UTF8.GetByteCount(name);
				byte* nativeName = (byte*)Marshal.AllocHGlobal(byteCount + 1);
				int offset = Utils.GetUtf8(name, nativeName, byteCount);

				nativeName[offset] = 0;

				io.NativePtr->BackendPlatformName = nativeName;
				_managedAllocations.Add((IntPtr)nativeName);
			}
		}

		internal static void SetIniFilename(this ImGuiIOPtr io, string name)
		{
			if (io.NativePtr->IniFilename != (byte*)0)
			{
				if (_managedAllocations.Contains((IntPtr)io.NativePtr->IniFilename))
				{
					Marshal.FreeHGlobal((IntPtr)io.NativePtr->IniFilename);
				}
				io.NativePtr->IniFilename = (byte*)0;
			}
			if (name != null)
			{
				int byteCount = Encoding.UTF8.GetByteCount(name);
				byte* nativeName = (byte*)Marshal.AllocHGlobal(byteCount + 1);
				int offset = Utils.GetUtf8(name, nativeName, byteCount);

				nativeName[offset] = 0;

				io.NativePtr->IniFilename = nativeName;
				_managedAllocations.Add((IntPtr)nativeName);
			}
		}

		public static void SetBackendRendererName(this ImGuiIOPtr io, string name)
		{
			if (io.NativePtr->BackendRendererName != (byte*)0)
			{
				if (_managedAllocations.Contains((IntPtr)io.NativePtr->BackendRendererName))
				{
					Marshal.FreeHGlobal((IntPtr)io.NativePtr->BackendRendererName);
					io.NativePtr->BackendRendererName = (byte*)0;
				}
			}
			if (name != null)
			{
				int byteCount = Encoding.UTF8.GetByteCount(name);
				byte* nativeName = (byte*)Marshal.AllocHGlobal(byteCount + 1);
				int offset = Utils.GetUtf8(name, nativeName, byteCount);

				nativeName[offset] = 0;

				io.NativePtr->BackendRendererName = nativeName;
				_managedAllocations.Add((IntPtr)nativeName);
			}
		}

		public static UnityEngine.Vector2 Convert(this System.Numerics.Vector2 vec) => new(vec.X, vec.Y);
		public static System.Numerics.Vector2 Convert(this UnityEngine.Vector2 vec) => new(vec.x, vec.y);
		public static UnityEngine.Vector3 Convert(this System.Numerics.Vector3 vec) => new(vec.X, vec.Y, vec.Z);
		public static System.Numerics.Vector3 Convert(this UnityEngine.Vector3 vec) => new(vec.x, vec.y, vec.z);
		public static UnityEngine.Vector4 Convert(this System.Numerics.Vector4 vec) => new(vec.X, vec.Y, vec.Z, vec.W);
		public static System.Numerics.Vector4 Convert(this UnityEngine.Vector4 vec) => new(vec.x, vec.y, vec.z, vec.w);
    }
}