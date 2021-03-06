﻿using System;
using OpenTK.Graphics.OpenGL;

namespace Framework
{
	public class ShaderException : Exception
	{
		public string Type { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ShaderException"/> class.
		/// </summary>
		/// <param name="msg">The error msg.</param>
		public ShaderException(string type, string msg) : base(msg)
		{
			Type = type;
		}
	}

	/// <summary>
	/// Shader class
	/// </summary>
	public class Shader : IDisposable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Shader"/> class.
		/// </summary>
		public Shader()
		{
			m_ProgramID = GL.CreateProgram();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			if (0 != m_ProgramID)
			{
				GL.DeleteProgram(m_ProgramID);
			}
		}

		public void Compile(string sShader, ShaderType type)
		{
			isLinked = false;
			int shaderObject = GL.CreateShader(type);
			if (0 == shaderObject) throw new ShaderException(type.ToString(), "Could not create.");
			// Compile vertex shader
			GL.ShaderSource(shaderObject, sShader);
			GL.CompileShader(shaderObject);
			int status_code;
			GL.GetShader(shaderObject, ShaderParameter.CompileStatus, out status_code);
			if (1 != status_code)
			{
				string log = CorrectLineEndings(GL.GetShaderInfoLog(shaderObject));
				throw new ShaderException(type.ToString(), log);
			}
			GL.AttachShader(m_ProgramID, shaderObject);
			//shaderIDs.Add(shaderObject);
		}

		/// <summary>
		/// Begins this shader use.
		/// </summary>
		public void Begin()
		{
			GL.UseProgram(m_ProgramID);
		}

		/// <summary>
		/// Ends this shader use.
		/// </summary>
		public void End()
		{
			GL.UseProgram(0);
		}

		public int GetAttributeLocation(string name)
		{
			return GL.GetAttribLocation(m_ProgramID, name);
		}

		public int GetUniformLocation(string name)
		{
			return GL.GetUniformLocation(m_ProgramID, name);
		}

		public bool IsLinked { get { return isLinked; } }

		public void Link()
		{
			try
			{
				GL.LinkProgram(m_ProgramID);
			}
			catch (Exception)
			{
				throw new ShaderException("Link", "Unknown error!");
			}
			int status_code;
			GL.GetProgram(m_ProgramID, GetProgramParameterName.LinkStatus, out status_code);
			if (1 != status_code)
			{
				string log = CorrectLineEndings(GL.GetProgramInfoLog(m_ProgramID));
				throw new ShaderException("Link", log);
			}
			isLinked = true;
		}

		private int m_ProgramID = 0;
		private bool isLinked = false;

		//private List<int> shaderIDs = new List<int>();

		private static string CorrectLineEndings(string input)
		{
			return input.Replace("\n", Environment.NewLine);
		}

		//private void DetachShaders()
		//{
		//	foreach (int id in shaderIDs)
		//	{
		//		GL.DetachShader(m_ProgramID, id);
		//	}
		//	shaderIDs.Clear();
		//}
	}
}
