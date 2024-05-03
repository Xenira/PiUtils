using System.Linq;

namespace PiUtils.Patches;

/// <summary>
/// A patch that applies a list of patches in order.
/// Once at least one of the patches is applied successfully, this patch is considered applied.
///
/// Each patch is applied at least once, even if another patch in the list has already been applied successfully.
/// </summary>
public class AtLeastOnePatch : IPatch
{
	private bool applied = false;
	private IPatch[] patches;

	public AtLeastOnePatch(IPatch[] patches)
	{
		this.patches = patches;
	}

	public bool Apply()
	{
		foreach (var patch in patches)
		{
			patch.Apply();
		}

		applied = patches.Select(p => p.IsApplied()).Any(applied => applied);

		return applied;
	}

	public bool IsApplied()
	{
		return applied;
	}
}
