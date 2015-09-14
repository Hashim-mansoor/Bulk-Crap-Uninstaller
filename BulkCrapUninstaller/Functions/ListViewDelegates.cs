﻿using System;
using BulkCrapUninstaller.Properties;
using Klocman.Extensions;
using Klocman.IO;
using UninstallTools;
using UninstallTools.Uninstaller;

namespace BulkCrapUninstaller.Functions
{
    internal static class ListViewDelegates
    {
        internal static string AspectToStringConverter(object x)
        {
            return x is long ? new FileSize((long) x).ToString() : string.Empty;
        }

        internal static string BoolToYesNoAspectConverter(object rowObject)
        {
            if (rowObject is bool)
            {
                var entry = (bool) rowObject;
                return entry.ToYesNo();
            }
            return null;
        }

        internal static object ColumnGuidAspectGetter(object rowObj)
        {
            var entry = rowObj as ApplicationUninstallerEntry;
            if (entry != null)
            {
                var result = entry.BundleProviderKey;
                 if(!result.IsEmpty())
                    return string.Format("{0:B}", result).ToUpper();
            }
            return string.Empty;
        }

        internal static object ColumnGuidGroupKeyGetter(object rowObj)
        {
            var entry = rowObj as ApplicationUninstallerEntry;
            if (entry != null)
            {
                var result = entry.BundleProviderKey;
                 if(result.Equals(Guid.Empty))
                    return Localisable.GuidFound;
            }
            return Localisable.GuidMissing;
        }

        internal static object ColumnInstallLocationGroupKeyGetter(object rowObj)
        {
            var entry = rowObj as ApplicationUninstallerEntry;
            return ApplicationUninstallerEntry.GetFuzzyDirectory(entry?.InstallLocation);
        }

        internal static object ColumnInstallSourceGroupKeyGetter(object rowObj)
        {
            var entry = rowObj as ApplicationUninstallerEntry;
            return ApplicationUninstallerEntry.GetFuzzyDirectory(entry?.InstallSource);
        }

        internal static object ColumnPublisherGroupKeyGetter(object rowObj)
        {
            var entry = rowObj as ApplicationUninstallerEntry;
            return string.IsNullOrEmpty(entry?.PublisherTrimmed) ? Localisable.Unknown : entry.PublisherTrimmed;
        }

        internal static object ColumnQuietUninstallStringGroupKeyGetter(object rowObj)
        {
            var entry = rowObj as ApplicationUninstallerEntry;
            return ApplicationUninstallerEntry.GetFuzzyDirectory(entry?.QuietUninstallString);
        }

        internal static object ColumnSizeAspectGetter(object x)
        {
            var applicationUninstallerEntry = x as ApplicationUninstallerEntry;
            if (applicationUninstallerEntry != null)
                return applicationUninstallerEntry.EstimatedSize.GetRawSize();
            return (long)0;
        }

        internal static object ColumnUninstallStringGroupKeyGetter(object rowObj)
        {
            var entry = rowObj as ApplicationUninstallerEntry;
            return ApplicationUninstallerEntry.GetFuzzyDirectory(entry?.UninstallString);
        }

        /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
        internal static object GetFirstCharGroupKeyGetter(object rowObj)
        {
            var entry = rowObj as ApplicationUninstallerEntry;
            return string.IsNullOrEmpty(entry?.DisplayName)
                ? Localisable.Empty
                : entry.DisplayName.TrimStart().Substring(0, 1).StripAccents().ToUpper();
        }

        internal static object DisplayVersionGroupKeyGetter(object rowObject)
        {
            var entry = rowObject as ApplicationUninstallerEntry;
            if (string.IsNullOrEmpty(entry?.DisplayVersion))
                return Localisable.Unknown;

            var dotIndex = entry.DisplayVersion.IndexOf('.');
            return dotIndex > 0 ? entry.DisplayVersion.Substring(0, dotIndex) + ".x" : entry.DisplayVersion;
        }

        internal static object ColumnSizeGroupKeyGetter(object rowObject)
        {
            var entry = rowObject as ApplicationUninstallerEntry;
            return entry == null || entry.EstimatedSize == FileSize.Empty
                ? Localisable.Unknown
                : "x " + entry.EstimatedSize.GetUnitName();
        }
    }
}