using System;

namespace PiUtils.Util;

public class License
{
	public static void LogLicense(PluginLogger logger, String author, String name, String version)
	{
		logger.LogInfo($"{name} {'v' + version} Copyright (C) {DateTime.Now.Year} {author}");
		logger.LogInfo("This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License version 3 as published by the Free Software Foundation.");
		logger.LogInfo("This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.");
		logger.LogInfo("You should have received a copy of the GNU General Public License along with this program. If not, see <https://www.gnu.org/licenses/>.");
	}
}
