package md52af0a16dc3d05e90842a46cda4fdae17;


public class MedioPagoActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("CondominioApp.MedioPagoActivity, CondominioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MedioPagoActivity.class, __md_methods);
	}


	public MedioPagoActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MedioPagoActivity.class)
			mono.android.TypeManager.Activate ("CondominioApp.MedioPagoActivity, CondominioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
