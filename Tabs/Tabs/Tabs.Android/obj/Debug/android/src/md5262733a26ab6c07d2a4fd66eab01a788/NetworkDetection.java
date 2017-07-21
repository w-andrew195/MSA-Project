package md5262733a26ab6c07d2a4fd66eab01a788;


public class NetworkDetection
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Tabs.NetworkDetection, Tabs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", NetworkDetection.class, __md_methods);
	}


	public NetworkDetection () throws java.lang.Throwable
	{
		super ();
		if (getClass () == NetworkDetection.class)
			mono.android.TypeManager.Activate ("Tabs.NetworkDetection, Tabs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
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
